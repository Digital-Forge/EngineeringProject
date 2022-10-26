import { environment } from "src/environments/environment";

export interface Client {
    id: string,
    name: string,
    description: string,
    comments: string,
    nip: string,
    address:string,
    contacts?: IClientContact[]
}

export interface IClientContact {
    id: string,
    firstname: string,
    surname: string,
    phone: string,
    email: string
}

export class ClientContact implements IClientContact {
    public id: string ;
    public firstname: string;
    public surname: string;
    public phone: string;
    public email: string;

    constructor(firstname: string, surname:string, phone:string, email: string){
        this.id=environment.emptyGuid;
        this.firstname=firstname;
        this.surname=surname;
        this.phone=phone;
        this.email=email;
    }
}