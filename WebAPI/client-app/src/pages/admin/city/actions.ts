import { Dispatch } from "react"

import http from "../../../http_comon"
import {
    ICity,
    ICityInfo,
    CityAction,
    CityActionTypes,
} from "./types"
import { ServerError } from "../../../store/types"

export const GetByIdCity = (id: number) => {
    return async (dispatch: Dispatch<CityAction>) => {
        try {
            let response = await http.get<ICityInfo>(`api/City/GetCityById/${id}`)

            dispatch({
                type: CityActionTypes.GET_BY_ID_CITY,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetCities = () => {
    return async (dispatch: Dispatch<CityAction>) => {
        try {
            let response = await http.get<Array<ICityInfo>>(`api/City/GetCities`)

            dispatch({
                type: CityActionTypes.GET_CITIES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateCity = (data: ICity) => {
    return async () => {
        try {
            await http.post("api/City/CreateCity", data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateCity = (id: number, data: ICity) => {
    return async () => {
        try {
            await http.put<ICity>(`api/City/UpdateCity/${id}`, data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteCity = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/City/DeleteCity/${id}`);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}