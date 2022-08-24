import { Address } from "./address.model";

export interface AppUser {
    id: string,
    fullName: string,
    pesel: number,
    address: Address
}