import { Department } from './department.model';
import { NoteStatus } from "./noteStatus.enum";

export interface Note {
    id: string,
    title: string,
    isCompany: boolean,
    noteStatus: string | null,
    date: Date
    createdBy: string
}

export interface NoteResponse {
    note: Note,
    statusName: string | null,
    author: string | null
}