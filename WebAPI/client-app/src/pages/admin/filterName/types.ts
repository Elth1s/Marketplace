export enum FilterNameActionTypes {
    GET_FILTER_NAMES = "GET_FILTER_NAMES",
    GET_BY_ID_FILTER_NAME = "GET_BY_ID_FILTER_NAME",
    CREATE_FILTER_NAME = "CREATE_FILTER_NAME",
    UPDATE_FILTER_NAME = "UPDATE_FILTER_NAME",
    SEARCH_FILTER_NAMES = "SEARCH_FILTER_NAMES",
}

export interface IFilterName {
    name: string,
    filterGroupId: number,
    unitId: number | null,
}

export interface IFilterNameInfo {
    id: number,
    name: string,
    filterGroupName: string,
    unitMeasure: string,
}

export interface ISearchFilterNames {
    count: number,
    values: Array<IFilterNameInfo>
}

export interface FilterNameState {
    selectedFilterName: IFilterName,
    count: number,
    filterNames: Array<IFilterNameInfo>
}


export interface GetFilterNamesAction {
    type: FilterNameActionTypes.GET_FILTER_NAMES,
    payload: Array<IFilterNameInfo>
}

export interface SearchFilterNamesAction {
    type: FilterNameActionTypes.SEARCH_FILTER_NAMES,
    payload: ISearchFilterNames
}

export interface GetByIdFilterNameAction {
    type: FilterNameActionTypes.GET_BY_ID_FILTER_NAME,
    payload: IFilterName
}

export interface CreateFilterNameAction {
    type: FilterNameActionTypes.CREATE_FILTER_NAME,
    payload: IFilterName
}

export interface UpdateFilterNameAction {
    type: FilterNameActionTypes.UPDATE_FILTER_NAME,
    payload: IFilterName
}


export type FilterNameAction = GetFilterNamesAction |
    SearchFilterNamesAction |
    GetByIdFilterNameAction |
    CreateFilterNameAction |
    UpdateFilterNameAction;