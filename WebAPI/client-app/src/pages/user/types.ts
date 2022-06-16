export enum ProfileActionTypes {
    GET_PROFILE = "GET_PROFILE",
}
export enum ConfirmEmailActionTypes {
    CONFIRM_EMAIL = "CONFIRM_EMAIL",
    SEND_CONFIRM_EMAIL = "SEND_CONFIRM_EMAIL",
    IS_EMAIL_CONFIRMED = "IS_EMAIL_CONFIRMED"
}
export enum ResetPasswordActionTypes {
    RESET_PASSWORD = "RESET_PASSWORD",
    RESET_CHANGE_PASSWORD = "RESET_CHANGE_PASSWORD"
}

export interface IProfile {
    firstName: string,
    secondName: string,
    userName: string,
    photo: string,
    isEmailConfirmed: boolean
}


export interface ProfileState {
    userInfo: IProfile
}

export interface ProfileServerError {
    title: string,
    status: number,
    errors: Array<any>,
}


//Confirm Email
export interface IConfirmEmail {
    userId: string,
    confirmationCode: string
}
export interface ConfirmEmailServerError {
    title: string,
    status: number,
    errors: Array<any>,
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

//Reset Password
export interface IResetPassword {
    email: string
}
export interface IResetChangePassword {
    userId: string,
    token: string,
    password: string,
    confirmPassword: string
}
export interface ResetPasswordServerError {
    title: string,
    status: number,
    errors: Array<any>,
}
export interface ResetPasswordAction {
    type: ResetPasswordActionTypes.RESET_PASSWORD,
}
export interface ResetChangePasswordAction {
    type: ResetPasswordActionTypes.RESET_CHANGE_PASSWORD,
}

export interface GetProfileAction {
    type: ProfileActionTypes.GET_PROFILE,
    payload: IProfile
}


export type ProfileAction = GetProfileAction;
export type EmailConfirmAction = ConfirmEmailAction | SendConfirmEmailAction | IsEmailConfirmedAction;
export type PasswordResetAction = ResetPasswordAction | ResetChangePasswordAction;