export enum UserActionTypes {
    GET_USERS = "GET_USERS",
    SEARCH_USERS = "SEARCH_USERS"
}

export interface IUserInfo {
    id: number,
    firstName: string,
    secondName: string,
    photo: string,
    email: number,
    phone: string,
}

export interface ISearchUsers {
    count: number,
    values: Array<IUserInfo>
}

export interface UserState {
    count: number,
    users: Array<IUserInfo>
}


export interface GetUsersAction {
    type: UserActionTypes.GET_USERS,
    payload: Array<IUserInfo>
}

export interface SearchUsersAction {
    type: UserActionTypes.SEARCH_USERS,
    payload: ISearchUsers
}


export type UserAction = GetUsersAction |
    SearchUsersAction