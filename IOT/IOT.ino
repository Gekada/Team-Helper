#include <Arduino.h>
#include <WiFi.h>
#include <WebServer.h>
#include <ArduinoJson.h>
#include <HTTPClient.h>
#include "DHT.h"

#define USE_SERIAL Serial
#define DHT_PIN 15
#define DHTTYPE DHT11

String apiString = "http://192.168.50.195:85";
String createGearEndpoint = "/api/Gear/Create";
String createIndicatorsDataId = "/api/IndicatorsData/Create";
String postGearPayload = "{\"name\":\"IOTGear\"}";
String indicatorsId;
String gearId;

float temperature;
float pressure;
float pulse;

StaticJsonDocument<250> jsonDocument;
char buffer[250];

WebServer server(80);

DHT dht(DHT_PIN, DHTTYPE);

void create_json(char *tag, float value, char *unit) {  
  jsonDocument.clear();  
  jsonDocument["type"] = tag;
  jsonDocument["value"] = value;
  jsonDocument["unit"] = unit;
  serializeJson(jsonDocument, buffer);
}

void createIndicatorsDataJson(String pulse, String temperature, String bloodpressure, String indicatorsId) {  
  jsonDocument.clear();  
  jsonDocument["pulse"] = pulse;
  jsonDocument["temperature"] = temperature;
  jsonDocument["bloodpressure"] = bloodpressure;
  jsonDocument["indicatorsId"] = indicatorsId;
  serializeJson(jsonDocument, buffer);
}

void add_json_object(char *tag, float value, char *unit) {
  JsonObject obj = jsonDocument.createNestedObject();
  obj["type"] = tag;
  obj["value"] = value;
  obj["unit"] = unit; 
}

void RegisterIndicators(){
  Serial.println("Got a post request");

  if (server.hasArg("plain") == false) {
    Serial.println("Bad request");
    server.send(400, "application/json", "{}");
    return;
  }
  
  String body = server.arg("plain");

  deserializeJson(jsonDocument, body);

  indicatorsId = jsonDocument["indicatorsId"].as<String>();

  Serial.println("Got indicatorsId: ");
  Serial.println(indicatorsId+'\n');

  server.send(200, "application/json", "{}");
}

void StopIndication(){
  Serial.println("Got a stop indication request");
  indicatorsId.clear();
  server.send(200, "application/json", "{}");
}

void GetTemperature(){
  Serial.println("Get temperature");
  create_json("temperature", temperature, "°C");
  server.send(200, "application/json", buffer);
}

void GetPulse(){
  Serial.println("Get pulse");
  create_json("pulse", temperature, "bpm");
  server.send(200, "application/json", buffer);
}

void GetPressure(){
  Serial.println("Get pressure");
  create_json("pressure", pressure, "hPa");
  server.send(200, "application/json", buffer);
}

void SendIndicatorsData(float temperature, float pressure, float pulse){
  if((WiFi.status() == WL_CONNECTED)) {
        HTTPClient http;

        USE_SERIAL.print("[HTTP] sending indicators data...\n");

        http.begin(apiString+createIndicatorsDataId); //HTTP

        createIndicatorsDataJson((String)temperature, (String)pressure, (String)pulse, indicatorsId);

        USE_SERIAL.print("[HTTP] POST...\n");
        http.addHeader("Content-Type", "application/json");
        int httpCode = http.POST(buffer);

        if(httpCode > 0) {
            USE_SERIAL.printf("[HTTP] POST... code: %d\n", httpCode);

            if(httpCode == HTTP_CODE_OK) {
                gearId = http.getString();
                USE_SERIAL.println(gearId);
            }
        } else {
            USE_SERIAL.printf("[HTTP] POST... failed, error: %s\n", http.errorToString(httpCode).c_str());
        }

        http.end();
    }
}

void setup_routing() {     
  server.on("/temperature", GetTemperature);     
  server.on("/pressure", GetPressure);     
  server.on("/pulse", GetPulse);     
  server.on("/setIndicators", HTTP_POST, RegisterIndicators);
  server.on("/stopIndicators", HTTP_POST, StopIndication);     
          
  server.begin();    
}

void setup() {
  WiFi.begin("Wifi", "pass");
  //add our device to db
  Serial.begin(115200);
  USE_SERIAL.print("Starting server on 192.168.50.247:80 \n");
  delay(3000);
  if((WiFi.status() == WL_CONNECTED)) {
        setup_routing();
        USE_SERIAL.print("Local IP: ");
        USE_SERIAL.print(WiFi.localIP());
        USE_SERIAL.print("\n");

        HTTPClient http;

        USE_SERIAL.print("[HTTP] begin to initialize node...\n");

        http.begin(apiString+createGearEndpoint); //HTTP

        USE_SERIAL.print("[HTTP] POST...\n");
        http.addHeader("Content-Type", "application/json");
        int httpCode = http.POST(postGearPayload);

        if(httpCode > 0) {
            USE_SERIAL.printf("[HTTP] POST... code: %d\n", httpCode);

            if(httpCode == HTTP_CODE_OK) {
                gearId = http.getString();
                USE_SERIAL.println(gearId);
            }
        } else {
            USE_SERIAL.printf("[HTTP] POST... failed, error: %s\n", http.errorToString(httpCode).c_str());
        }

        http.end();
    }
}

void loop() {
    server.handleClient();

    temperature = dht.readTemperature();

    pressure = dht.readHumidity();

    pulse = (temperature+pressure)/2;

    USE_SERIAL.print("Temperature: ");
    USE_SERIAL.print(temperature);
    USE_SERIAL.print('\n');

    USE_SERIAL.print("Blood pressure: ");
    USE_SERIAL.print(pressure);
    USE_SERIAL.print('\n');

    USE_SERIAL.print("Pulse: ");
    USE_SERIAL.print(pulse);
    USE_SERIAL.print('\n');

    if (!gearId.isEmpty() && !indicatorsId.isEmpty()){
      SendIndicatorsData(temperature, pressure, pulse);
    }
    delay(3000);
}
