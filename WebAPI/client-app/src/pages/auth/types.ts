export enum AuthActionTypes {
    AUTH_DIALOG_CHANGE = "AUTH_DIALOG_CHANGE",
    AUTH_SUCCESS = "AUTH_SUCCESS",
    AUTH_LOGOUT = "AUTH_LOGOUT"
}

export interface IUser {
    name: string,
    emailOrPhone: string,
    isEmailExist: boolean,
    role: string,
    photo: string,
}

export interface IAuthResponse {
    accessToken: string,
    refreshToken: string,
}

export interface AuthState {
    isAuthDialogOpen: boolean,
    user: IUser,
    isAuth: boolean,
}

export interface ILoginModel {
    emailOrPhone: string,
    password: string,
}

export interface IRegisterModel {
    firstName: string,
    secondName: string,
    emailOrPhone: string,
    password: string,
};

export interface IExternalLoginModel {
    token: string
};

export interface AuthDialogChangeAction {
    type: AuthActionTypes.AUTH_DIALOG_CHANGE
}

export interface AuthSuccessAction {
    type: AuthActionTypes.AUTH_SUCCESS,
    payload: IUser
}

export interface AuthLogOut {
    type: AuthActionTypes.AUTH_LOGOUT
}

export type AuthAction = AuthDialogChangeAction | AuthSuccessAction | AuthLogOut;