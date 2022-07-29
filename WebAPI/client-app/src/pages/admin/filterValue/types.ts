export enum FilterValueActionTypes {
    GET_FILTER_VALUES = "GET_FILTER_VALUES",
    GET_BY_ID_FILTER_VALUE = "GET_BY_ID_FILTER_VALUE",
    CREATE_FILTER_VALUE = "CREATE_FILTER_VALUE",
    UPDATE_FILTER_VALUE = "UPDATE_FILTER_VALUE",
    SEARCH_FILTER_VALUE = "SEARCH_FILTER_VALUE",
}

export interface IFilterValue {
    value: string,
    min: number | string | null,
    max: number | string | null,
    filterNameId: number,
}

export interface IFilterValueInfo {
    id: number,
    value: string,
    filterName: string,
    min: number | null,
    max: number | null,
}

export interface ISearchFilterValues {
    count: number,
    values: Array<IFilterValueInfo>
}

export interface FilterValueState {
    selectedFilterValue: IFilterValue,
    count: number,
    filterValues: Array<IFilterValueInfo>
}


export interface GetFilterValuesAction {
    type: FilterValueActionTypes.GET_FILTER_VALUES,
    payload: Array<IFilterValueInfo>
}

export interface SearchFilterValuesAction {
    type: FilterValueActionTypes.SEARCH_FILTER_VALUE,
    payload: ISearchFilterValues
}

export interface GetByIdFilterValueAction {
    type: FilterValueActionTypes.GET_BY_ID_FILTER_VALUE,
    payload: IFilterValue
}

export interface CreateFilterValueAction {
    type: FilterValueActionTypes.CREATE_FILTER_VALUE,
    payload: IFilterValue
}

export interface UpdateFilterValueAction {
    type: FilterValueActionTypes.UPDATE_FILTER_VALUE,
    payload: IFilterValue
}


export type FilterValueAction = GetFilterValuesAction |
    SearchFilterValuesAction |
    GetByIdFilterValueAction |
    CreateFilterValueAction |
    UpdateFilterValueAction;