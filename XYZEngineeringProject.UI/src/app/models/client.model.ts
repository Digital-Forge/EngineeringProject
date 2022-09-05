export interface Client {
    firstName: string,

}

export interface ClientResponse extends Response {
    data: Client[]
}