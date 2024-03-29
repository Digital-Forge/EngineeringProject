import { Department } from 'src/app/models/department.model';
import { Address } from "./address.model";

export interface User {
    id: string,
    userName: string,
    passwordHash: string,
    name: string,
    surname: string,
    pesel: string,
    address: Address,
    roles: string[],
    departments?: Department[]
}