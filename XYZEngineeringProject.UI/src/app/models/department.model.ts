import { User } from "./user.model"

export interface Department {
    id: string,
    name: string
    managerId:string
    users: User[]
}

export interface DepartmentManager {
    department: Department,
    manager: User
}

