export interface Message {
    id: string,
    text: string,
    author: string,
    authorId: string,
    date: Date
}

export interface NewMessage {
    forumId: string,
    userId: string,
    text: string
}

export interface Forum {
    id: string,
    name: string,
    messages: Message[]
}
