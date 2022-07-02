export enum AuthActionTypes {
    AUTH_SUCCESS = "AUTH_SUCCESS",
    AUTH_LOGOUT = "AUTH_LOGOUT"
}

export interface IUser {
    name: string,
    emailOrPhone: string,
    role: string,
    photo: string,
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

export interface AuthSuccessAction {
    type: AuthActionTypes.AUTH_SUCCESS,
    payload: IUser
}

export interface AuthLogOut {
    type: AuthActionTypes.AUTH_LOGOUT
}

export type AuthAction = AuthSuccessAction | AuthLogOut;