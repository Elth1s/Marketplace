export enum CountryActionTypes {
    GET_COUNTRIES = "GET_COUNTRIES",
    GET_BY_ID_COUNTRY = "GET_BY_ID_COUNTRY",
    CREATE_COUNTRY = "CREATE_COUNTRY",
    UPDATE_COUNTRY = "UPDATE_COUNTRY",
}

export interface ICountry {
    name: string
}

export interface ICountryInfo {
    id: number
    name: string
}

export interface CountryState {
    countryInfo: ICountryInfo,
    countries: Array<ICountryInfo>
}


export interface ICountryUpdatePage {
    id: number
}


export interface GetCountrysAction {
    type: CountryActionTypes.GET_COUNTRIES,
    payload: Array<ICountryInfo>
}

export interface GetByIdCountryAction {
    type: CountryActionTypes.GET_BY_ID_COUNTRY,
    payload: ICountryInfo
}

export interface CreateCountryAction {
    type: CountryActionTypes.CREATE_COUNTRY,
    payload: ICountry
}

export interface UpdateCountryAction {
    type: CountryActionTypes.UPDATE_COUNTRY,
    payload: ICountry
}


export type CountryAction = GetCountrysAction |
    GetByIdCountryAction |
    CreateCountryAction |
    UpdateCountryAction;