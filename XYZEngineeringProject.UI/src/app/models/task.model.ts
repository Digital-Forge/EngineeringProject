import { TaskListStatus } from './taskListStatus.enum';
import { Priority } from "./priority.enum";

export interface Task {
    id: string,
    deadline: Date,
    priority: Priority,
    title: string,
    description?: string,
    assigneeUserId?: string,
    assignerUserId:string,
    listOfTasksId?: string,
    createDate: Date,
    isComplete: boolean,
    createBy: string
}

export interface TaskList {
    id: string,
    name: string,
    createDate: Date,
    project?: string,
    status: TaskListStatus,
    tasks?: Task[],
    createBy: string
}

export interface TaskListResponse {
    taskListData: TaskList,
    tasks?: Task[],
    isListComplete?: boolean
}