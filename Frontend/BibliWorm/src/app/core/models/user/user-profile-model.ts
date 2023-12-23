import { Role } from "../../enums/role";

export interface UserProfileModel {
    userId: number,
    firstName: string,
    lastName: string,
    profilePicture: string,
    address: string,
    phoneNumber: string,
    birthDate: Date,
    birthDateString: string,
    email: string,
    login: string,
    registrationDate: Date,
    role: Role
}
