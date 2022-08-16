export enum UnitActionTypes {
    GET_UNITS = "GET_UNITS",
    GET_BY_ID_UNIT = "GET_BY_ID_UNIT",
    CREATE_UNIT = "CREATE_UNIT",
    UPDATE_UNIT = "UPDATE_UNIT",
    SEARCH_UNITS = "SEARCH_UNITS"
}

export interface IUnit {
    englishMeasure: string,
    ukrainianMeasure: string,
}

export interface IUnitInfo {
    id: number,
    measure: string,
}

export interface ISearchUnits {
    count: number,
    values: Array<IUnitInfo>
}

export interface UnitState {
    selectedUnit: IUnit,
    count: number,
    units: Array<IUnitInfo>
}



export interface GetUnitsAction {
    type: UnitActionTypes.GET_UNITS,
    payload: Array<IUnitInfo>
}

export interface SearchUnitsAction {
    type: UnitActionTypes.SEARCH_UNITS,
    payload: ISearchUnits
}

export interface GetByIdUnitAction {
    type: UnitActionTypes.GET_BY_ID_UNIT,
    payload: IUnit
}

export interface CreateUnitAction {
    type: UnitActionTypes.CREATE_UNIT,
    payload: IUnit
}

export interface UpdateUnitAction {
    type: UnitActionTypes.UPDATE_UNIT,
    payload: IUnit
}


export type UnitAction = GetUnitsAction |
    SearchUnitsAction |
    GetByIdUnitAction |
    CreateUnitAction |
    UpdateUnitAction;