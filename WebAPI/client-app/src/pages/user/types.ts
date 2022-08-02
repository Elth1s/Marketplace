export enum ProfileActionTypes {
    GET_PROFILE = "GET_PROFILE",
}
export enum ConfirmEmailActionTypes {
    CONFIRM_EMAIL = "CONFIRM_EMAIL",
    SEND_CONFIRM_EMAIL = "SEND_CONFIRM_EMAIL",
    IS_EMAIL_CONFIRMED = "IS_EMAIL_CONFIRMED"
}


export interface IProfile {
    firstName: string,
    secondName: string,

    gender: string,
    birthDate: Date | null,
    languageOfCommunication: number,
    region: number,
    address: string,
    city: string,
    postalCode: string,

    email: string,
    phone: string,

    photo: string,
    isEmailConfirmed: boolean,
    isPhoneConfirmed: boolean,
    hasPassword: boolean
}


export interface ProfileState {
    userInfo: IProfile,
}


//Confirm Email
export interface IConfirmEmail {
    userId: string,
    confirmationCode: string
}
export interface ConfirmEmailAction {
    type: ConfirmEmailActionTypes.CONFIRM_EMAIL,
}
export interface SendConfirmEmailAction {
    type: ConfirmEmailActionTypes.SEND_CONFIRM_EMAIL,
}
export interface IsEmailConfirmedAction {
    type: ConfirmEmailActionTypes.IS_EMAIL_CONFIRMED,
    payload: boolean
}


export interface GetProfileAction {
    type: ProfileActionTypes.GET_PROFILE,
    payload: IProfile
}


export type ProfileAction = GetProfileAction;
export type EmailConfirmAction = ConfirmEmailAction | SendConfirmEmailAction | IsEmailConfirmedAction;