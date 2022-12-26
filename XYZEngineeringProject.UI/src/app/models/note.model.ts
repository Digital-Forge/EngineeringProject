import { NoteStatus } from "./noteStatus.enum";

export interface Note {
    id: string,
    title: string,
    noteStatus: NoteStatus,
    date: Date
    createdBy: string
}