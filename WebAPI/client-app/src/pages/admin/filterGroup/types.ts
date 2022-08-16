export enum FilterGroupActionTypes {
    GET_FILTER_GROUPS = "GET_FILTER_GROUPS",
    GET_BY_ID_FILTER_GROUP = "GET_BY_ID_FILTER_GROUP",
    CREATE_FILTER_GROUP = "CREATE_FILTER_GROUP",
    UPDATE_FILTER_GROUP = "UPDATE_FILTER_GROUP",
    SEARCH_FILTER_GROUPS = "SEARCH_FILTER_GROUPS",
}

export interface IFilterGroup {
    englishName: string,
    ukrainianName: string,
}

export interface IFilterGroupInfo {
    id: number
    name: string
}

export interface ISearchFilterGroups {
    count: number,
    values: Array<IFilterGroupInfo>
}

export interface FilterGroupState {
    selectedFilterGroup: IFilterGroup,
    count: number,
    filterGroups: Array<IFilterGroupInfo>
}


export interface SearchFilterGroupsAction {
    type: FilterGroupActionTypes.SEARCH_FILTER_GROUPS,
    payload: ISearchFilterGroups
}

export interface GetFilterGroupsAction {
    type: FilterGroupActionTypes.GET_FILTER_GROUPS,
    payload: Array<IFilterGroupInfo>
}

export interface GetByIdFilterGroupAction {
    type: FilterGroupActionTypes.GET_BY_ID_FILTER_GROUP,
    payload: IFilterGroup
}

export interface CreateFilterGroupAction {
    type: FilterGroupActionTypes.CREATE_FILTER_GROUP,
    payload: IFilterGroup
}

export interface UpdateFilterGroupAction {
    type: FilterGroupActionTypes.UPDATE_FILTER_GROUP,
    payload: IFilterGroup
}


export type FilterGroupAction = SearchFilterGroupsAction |
    GetFilterGroupsAction |
    GetByIdFilterGroupAction |
    CreateFilterGroupAction |
    UpdateFilterGroupAction;