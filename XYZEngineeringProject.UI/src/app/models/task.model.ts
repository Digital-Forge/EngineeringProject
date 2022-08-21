import { Priority } from "./priority.enum";

export interface Task {
    id: string,
    deadline: Date,
    priority: Priority,
    title: string,
    description: string
}