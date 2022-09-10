export enum CityActionTypes {
    GET_CITIES = "GET_CITIES",
    GET_CITIES_FOR_SELECT = "GET_CITIES_FOR_SELECT",
    GET_BY_ID_CITY = "GET_BY_ID_CITY",
    CREATE_CITY = "CREATE_CITY",
    UPDATE_CITY = "UPDATE_CITY",
    SEARCH_CITIES = "SEARCH_CITIES"
}

export interface ICity {
    englishName: string,
    ukrainianName: string,
    countryId: number,
}

export interface ICityInfo {
    id: number,
    name: string,
    countryName: string,
}

export interface ICityForSelect {
    id: number,
    name: string,
}

export interface ISearchCities {
    count: number,
    values: Array<ICityInfo>
}

export interface CityState {
    selectedCity: ICity,
    count: number,
    cities: Array<ICityInfo>,
    cityForSelect: Array<ICityForSelect>
}


export interface GetCitiesAction {
    type: CityActionTypes.GET_CITIES,
    payload: Array<ICityInfo>
}

export interface GetCitiesForSelectAction {
    type: CityActionTypes.GET_CITIES_FOR_SELECT,
    payload: Array<ICityForSelect>
}

export interface SearchCitiesAction {
    type: CityActionTypes.SEARCH_CITIES,
    payload: ISearchCities
}

export interface GetByIdCityAction {
    type: CityActionTypes.GET_BY_ID_CITY,
    payload: ICity
}

export interface CreateCityAction {
    type: CityActionTypes.CREATE_CITY,
    payload: ICity
}

export interface UpdateCityAction {
    type: CityActionTypes.UPDATE_CITY,
    payload: ICity
}


export type CityAction = GetCitiesAction |
    GetCitiesForSelectAction |
    SearchCitiesAction |
    GetByIdCityAction |
    CreateCityAction |
    UpdateCityAction;