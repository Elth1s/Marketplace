export enum ResetPasswordActionTypes {
    RESET_PASSWORD = "RESET_PASSWORD",
    RESET_CHANGE_PASSWORD = "RESET_CHANGE_PASSWORD"
}


export interface IResetPasswordInfo {
    userId: string,
    token: string
}

export interface IResetPasswordEmail {
    email: string
}

export interface IResetPasswordPhone {
    phone: string,
    code: string
}

export interface IPhoneCodeRequest {
    phone: string
}

export interface IResetChangePassword {
    userId: string,
    token: string,
    password: string,
    confirmPassword: string
}


export interface ResetPasswordAction {
    type: ResetPasswordActionTypes.RESET_PASSWORD,
}
export interface ResetChangePasswordAction {
    type: ResetPasswordActionTypes.RESET_CHANGE_PASSWORD,
}

export type PasswordResetAction = ResetPasswordAction | ResetChangePasswordAction;

