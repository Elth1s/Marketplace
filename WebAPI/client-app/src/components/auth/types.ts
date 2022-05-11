export enum AuthActionTypes {
    AUTH_SUCCESS = "AUTH_SUCCESS",
    AUTH_LOGOUT = "AUTH_LOGOUT"
}

export interface IUser {
    id: string,
    firstName: string,
    secondName: string,
    photo: string,
    email: string,
    roles: string
}

export interface IAuthResponse {
    accessToken: string,
    refreshToken: string,
}

export interface AuthState {
    user: IUser,
    isAuth: boolean,
}

export interface ILoginModel {
    email: string,
    password: string,
}

export interface LoginServerError {
    title: string,
    status: number,
    errors: Array<any>
}

export interface IRegisterModel {
    firstName: string,
    secondName: string,
    phone: string,
    email: string,
    password: string,
    confirmPassword: string
};

export interface RegisterServerError {
    title: string,
    status: number,
    errors: Array<any>,
}

export interface IExternalLoginModel {
    provider: string,
    token: string
};
export interface ExternalLoginServerError {
    statusCode: string,
    error: string
}
export interface AuthSuccessAction {
    type: AuthActionTypes.AUTH_SUCCESS,
    payload: IUser
}

export interface AuthLogOut {
    type: AuthActionTypes.AUTH_LOGOUT
}

export type AuthAction = AuthSuccessAction | AuthLogOut;