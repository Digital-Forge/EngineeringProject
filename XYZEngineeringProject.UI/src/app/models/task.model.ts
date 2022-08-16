import { Priority } from "./priority.enum";

export interface Task {
    id: string,
    deadLine: Date,
    priority: Priority,
    title: string,
    description: string
}