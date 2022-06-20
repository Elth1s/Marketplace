export enum CityActionTypes {
    GET_CITIES = "GET_CITIES",
    GET_BY_ID_CITY = "GET_BY_ID_CITY",
    CREATE_CITY = "CREATE_CITY",
    UPDATE_CITY = "UPDATE_CITY",
}

export interface ICity {
    name: string,
    countryId: number | string,
}

export interface ICityInfo {
    id: number,
    name: string,
    countryName: string,
}

export interface CityState {
    cityInfo: ICityInfo,
    cities: Array<ICityInfo>
}


export interface ICityUpdatePage {
    id: number,
};


export interface GetCitysAction {
    type: CityActionTypes.GET_CITIES,
    payload: Array<ICityInfo>
}

export interface GetByIdCityAction {
    type: CityActionTypes.GET_BY_ID_CITY,
    payload: ICityInfo
}

export interface CreateCityAction {
    type: CityActionTypes.CREATE_CITY,
    payload: ICity
}

export interface UpdateCityAction {
    type: CityActionTypes.UPDATE_CITY,
    payload: ICity
}


export type CityAction = GetCitysAction |
    GetByIdCityAction |
    CreateCityAction |
    UpdateCityAction;