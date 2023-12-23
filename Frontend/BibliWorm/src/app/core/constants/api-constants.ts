import { Observable, OperatorFunction, tap } from "rxjs";
import { environment } from "../../../environments/environment.development";

const baseURL = environment.webApi;

const authURL = `${baseURL}Auth`;
const login = 'login';
const refresh = 'refresh';

const bookURL = `${baseURL}Book`;
const addBook = 'add-book';
const addBookCopy = 'add-book-copy';
const bookList = 'book-list';
const bookTheBookCopies = 'book-the-book-copies';
const deleteBook = 'delete-book';
const deleteBookCopy = 'delete-book-copy';
const getBookById = 'get-book';
const getBookCopies = 'get-book-copies';
const getBookCopyById = 'get-book-copy';
const getBookCopyByRFID = 'get-book-copy-by-rfid';
const getMostPopularBooks = 'get-most-popular-books';
const getMostPopularBooksInGenre = 'get-most-popular-books-in-genre';
const returnBookCopies = 'return-book-copies';
const updateBook = 'update-book';
const updateBookCopy = 'update-book-copy';
const getRandomBook = 'get-random-book';
const getRecomendationsForUser = 'get-recomendations-for-user';
const getUserBookings = 'get-user-bookings';

const departmentURL = `${baseURL}Department`;
const addDepartment = 'add';
const deleteDepartment = 'delete';
const getAllDepartments = 'get-all';
const getDepartmentById = 'get';
const updateDepartment = 'update';

const enumItemURL = `${baseURL}EnumItem`;
const getAllEnumItems = 'get-all';
const addEnumItem = 'add';
const deleteEnumItem = 'delete';
const getEnumItemById = 'get';
const updateEnumItem = 'update';

const userURL = `${baseURL}User`;
const register = 'register';
const getAllUsers = 'get-all';
const getUserProfileById = 'get';
const setUserRole = 'set-user-role';
const updateReaderCard = 'update';

const administrationURL = `${baseURL}Administration`;
const backupDatabase = 'backup-database';

export const api = {
    auth: {
        login: `${authURL}/${login}`,
        refresh: `${authURL}/${refresh}`,
    },
    book: {
        addBook: `${bookURL}/${addBook}`,
        addBookCopy: `${bookURL}/${addBookCopy}`,
        bookList: `${bookURL}/${bookList}`,
        bookTheBookCopies: `${bookURL}/${bookTheBookCopies}`,
        deleteBook: `${bookURL}/${deleteBook}`,
        deleteBookCopy: `${bookURL}/${deleteBookCopy}`,
        getBookById: `${bookURL}/${getBookById}`,
        getBookCopies: `${bookURL}/${getBookCopies}`,
        getBookCopyById: `${bookURL}/${getBookCopyById}`,
        getBookCopyByRFID: `${bookURL}/${getBookCopyByRFID}`,
        getMostPopularBooks: `${bookURL}/${getMostPopularBooks}`,
        getMostPopularBooksInGenre: `${bookURL}/${getMostPopularBooksInGenre}`,
        returnBookCopies: `${bookURL}/${returnBookCopies}`,
        updateBook: `${bookURL}/${updateBook}`,
        updateBookCopy: `${bookURL}/${updateBookCopy}`,
        getRandomBook: `${bookURL}/${getRandomBook}`,
        getRecomendationsForUser: `${bookURL}/${getRecomendationsForUser}`,
        getUserBookings: `${bookURL}/${getUserBookings}`
    },
    department: {
        addDepartment: `${departmentURL}/${addDepartment}`,
        deleteDepartment: `${departmentURL}/${deleteDepartment}`,
        getAllDepartments: `${departmentURL}/${getAllDepartments}`,
        getDepartmentById: `${departmentURL}/${getDepartmentById}`,
        updateDepartment: `${departmentURL}/${updateDepartment}`,
    },
    enumItem: {
        getAllEnumItems: `${enumItemURL}/${getAllEnumItems}`,
        addEnumItem: `${enumItemURL}/${addEnumItem}`,
        deleteEnumItem: `${enumItemURL}/${deleteEnumItem}`,
        getEnumItemById: `${enumItemURL}/${getEnumItemById}`,
        updateEnumItem: `${enumItemURL}/${updateEnumItem}`,
    },
    user: {
        register: `${userURL}/${register}`,
        getAllUsers: `${userURL}/${getAllUsers}`,
        getUserProfileById: `${userURL}/${getUserProfileById}`,
        setUserRole: `${userURL}/${setUserRole}`,
        updateReaderCard: `${userURL}/${updateReaderCard}`,
    },
    administration: {
        backupDatabase: `${administrationURL}/${backupDatabase}`,
    }
}

export function extractData<T>(): OperatorFunction<any, any> {
    return (source: Observable<T>) =>
    source.pipe(
        tap(data => data)
    );
}