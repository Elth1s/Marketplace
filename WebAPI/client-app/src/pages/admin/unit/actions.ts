import { Dispatch } from "react"

import http from "../../../http_comon"
import {
    ISearchUnits,
    IUnit,
    IUnitInfo,
    UnitAction,
    UnitActionTypes
} from "./types"
import { ServerError } from "../../../store/types"
import qs from "qs"

export const SearchUnits = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string) => {
    return async (dispatch: Dispatch<UnitAction>) => {
        try {
            let response = await http.get<ISearchUnits>(`api/Unit/SearchUnits`, {
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
                type: UnitActionTypes.SEARCH_UNITS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetUnitById = (id: number) => {
    return async (dispatch: Dispatch<UnitAction>) => {
        try {
            let response = await http.get<IUnit>(`api/Unit/GetById/${id}`)

            dispatch({
                type: UnitActionTypes.GET_BY_ID_UNIT,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetUnits = () => {
    return async (dispatch: Dispatch<UnitAction>) => {
        try {
            let response = await http.get<Array<IUnitInfo>>(`api/Unit/GetUnits`)

            dispatch({
                type: UnitActionTypes.GET_UNITS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateUnit = (data: IUnit) => {
    return async () => {
        try {
            await http.post("api/Unit/Create", data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateUnit = (id: number, data: IUnit) => {
    return async () => {
        try {
            await http.put<IUnit>(`api/Unit/Update/${id}`, data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteUnit = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/Unit/Delete/${id}`);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteUnits = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/Unit/DeleteUnits`, {
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