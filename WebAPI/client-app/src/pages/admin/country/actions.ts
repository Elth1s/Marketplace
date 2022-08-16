import { Dispatch } from "react"
import qs from "qs"

import http from "../../../http_comon"
import {
    ICountry,
    ICountryInfo,
    CountryAction,
    CountryActionTypes,
    ISearchCountries,
} from "./types"
import { ServerError } from "../../../store/types"

export const SearchCountries = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string) => {
    return async (dispatch: Dispatch<CountryAction>) => {
        try {
            let response = await http.get<ISearchCountries>(`api/Country/Search`, {
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
                type: CountryActionTypes.SEARCH_COUNTRIES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetCountries = () => {
    return async (dispatch: Dispatch<CountryAction>) => {
        try {
            let response = await http.get<Array<ICountryInfo>>(`api/Country/Get`)

            dispatch({
                type: CountryActionTypes.GET_COUNTRIES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetByIdCountry = (id: number) => {
    return async (dispatch: Dispatch<CountryAction>) => {
        try {
            let response = await http.get<ICountry>(`api/Country/GetById/${id}`)

            dispatch({
                type: CountryActionTypes.GET_BY_ID_COUNTRY,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateCountry = (data: ICountry) => {
    return async () => {
        try {
            await http.post("api/Country/Create", data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateCountry = (id: number, data: ICountry) => {
    return async () => {
        try {
            await http.put<ICountry>(`api/Country/Update/${id}`, data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteCountry = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/Country/Delete/${id}`);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteCountries = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/Country/Delete`, {
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