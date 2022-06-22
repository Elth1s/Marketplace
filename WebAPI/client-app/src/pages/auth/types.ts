export enum AuthActionTypes {
    AUTH_SUCCESS = "AUTH_SUCCESS",
    AUTH_LOGOUT = "AUTH_LOGOUT"
}

export interface IUser {
    username: string,
    email: string,
    photo: string,
    roles: string,
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

export interface IRegisterModel {
    firstName: string,
    secondName: string,
    email: string,
    password: string,
};

export interface IExternalLoginModel {
    provider: string,
    token: string
};

export interface AuthSuccessAction {
    type: AuthActionTypes.AUTH_SUCCESS,
    payload: IUser
}

export interface AuthLogOut {
    type: AuthActionTypes.AUTH_LOGOUT
}

export type AuthAction = AuthSuccessAction | AuthLogOut;