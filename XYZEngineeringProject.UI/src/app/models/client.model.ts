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
    id: any,
    firstname: any,
    surname: any,
    phone: any,
    email: any
}

export class ClientContact implements IClientContact {
    public id: any ;
    public firstname: any;
    public surname: any;
    public phone: any;
    public email: any;

    constructor(id: any, firstname: any, surname:any, phone:any, email: any){
        this.id=id;
        this.firstname=firstname;
        this.surname=surname;
        this.phone=phone;
        this.email=email;
    }
}