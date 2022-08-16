export enum CountryActionTypes {
    GET_COUNTRIES = "GET_COUNTRIES",
    GET_BY_ID_COUNTRY = "GET_BY_ID_COUNTRY",
    CREATE_COUNTRY = "CREATE_COUNTRY",
    UPDATE_COUNTRY = "UPDATE_COUNTRY",
    SEARCH_COUNTRIES = "SEARCH_COUNTRIES"
}

export interface ICountry {
    englishName: string,
    ukrainianName: string,
    code: string
}

export interface ICountryInfo {
    id: number,
    name: string,
    code: string
}

export interface ISearchCountries {
    count: number,
    values: Array<ICountryInfo>
}

export interface CountryState {
    selectedCountry: ICountry,
    count: number,
    countries: Array<ICountryInfo>
}


export interface GetCountriesAction {
    type: CountryActionTypes.GET_COUNTRIES,
    payload: Array<ICountryInfo>
}

export interface SearchCountriesAction {
    type: CountryActionTypes.SEARCH_COUNTRIES,
    payload: ISearchCountries
}

export interface GetByIdCountryAction {
    type: CountryActionTypes.GET_BY_ID_COUNTRY,
    payload: ICountry
}

export interface CreateCountryAction {
    type: CountryActionTypes.CREATE_COUNTRY,
    payload: ICountry
}

export interface UpdateCountryAction {
    type: CountryActionTypes.UPDATE_COUNTRY,
    payload: ICountry
}


export type CountryAction = GetCountriesAction |
    SearchCountriesAction |
    GetByIdCountryAction |
    CreateCountryAction |
    UpdateCountryAction;