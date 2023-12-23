export interface CreateUpdateBookModel {
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
    departmentId: string,
    genres: number[]
}
