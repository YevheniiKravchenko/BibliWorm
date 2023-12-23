import { KeyValue } from "@angular/common";

export interface BookModel {
    bookId: string,
    title: string,
    author: string,
    isbn: string,
    publicationDate: Date,
    publisher: string,
    description: string,
    pagesAmount: number,
    coverImage: string,
    keyWords: string,
    departmentId: number,
    bookGenres: number[],
    genres: KeyValue<number, string>[]
    departments: KeyValue<number, string>[]
}
