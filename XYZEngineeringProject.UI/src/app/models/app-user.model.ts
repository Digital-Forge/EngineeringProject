import { Address } from "./address.model";

export interface AppUser {
    id: string,
    userName: string,
    passwordHash: string,
    fullName: string,
    pesel: number,
    address: Address
}