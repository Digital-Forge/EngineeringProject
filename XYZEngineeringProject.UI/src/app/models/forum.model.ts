export interface Message {
    id: string,
    text: string,
    author: string,
    date: Date
}

export interface Forum {
    id: string,
    name: string,
    messages: Message[]
}