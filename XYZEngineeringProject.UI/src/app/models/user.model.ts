import { Address } from "./address.model";

export interface User {
    id: string,
    userName: string,
    passwordHash: string,
    name: string,
    surname: string,
    pesel: number,
    address: Address
}