export class ClientBase {
  protected constructor() {}

  protected transformOptions(options: any): Promise<any> {
    let authToken = localStorage.getItem('token');
    options.headers = options.headers.append(
      'Authorization',
      'Bearer ' + authToken
    );
    console.log(options);
    return Promise.resolve(options);
  }
}
