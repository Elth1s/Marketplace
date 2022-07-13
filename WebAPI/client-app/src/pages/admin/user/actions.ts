import { Dispatch } from "react"
import qs from "qs"

import http from "../../../http_comon"
import {
    IUserInfo,
    UserAction,
    UserActionTypes,
    ISearchUsers,
} from "./types"
import { ServerError } from "../../../store/types"

export const SearchUsers = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string) => {
    return async (dispatch: Dispatch<UserAction>) => {
        try {
            let response = await http.get<ISearchUsers>(`api/User/Search`, {
                params: {
                    page: page,
                    rowsPerPage: rowsPerPage,
                    name: name,
                    isAscOrder: isAscOrder,
                    orderBy: orderBy
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            })

            dispatch({
                type: UserActionTypes.SEARCH_USERS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetUsers = () => {
    return async (dispatch: Dispatch<UserAction>) => {
        try {
            let response = await http.get<Array<IUserInfo>>(`api/User/Get`)

            dispatch({
                type: UserActionTypes.GET_USERS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteUsers = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/User/Delete`, {
                params: {
                    ids: ids,
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            })
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}