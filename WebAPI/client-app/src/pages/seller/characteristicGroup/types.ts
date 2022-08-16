export enum CharacteristicGroupActionTypes {
    GET_CHARACTERISTIC_GROUPS = "GET_CHARACTERISTIC_GROUPS",
    GET_BY_ID_CHARACTERISTIC_GROUP = "GET_BY_ID_CHARACTERISTIC_GROUP",
    CREATE_CHARACTERISTIC_GROUP = "CREATE_CHARACTERISTIC_GROUP",
    UPDATE_CHARACTERISTIC_GROUP = "UPDATE_CHARACTERISTIC_GROUP",
    SEARCH_CHARACTERISTIC_GROUPS = "CHARACTERISTIC_GROUPS",
}

export interface ICharacteristicGroup {
    name: string
}

export interface ICharacteristicGroupInfo {
    id: number
    name: string
}

export interface ISearchCharacteristicGroups {
    count: number,
    values: Array<ICharacteristicGroupInfo>
}

export interface CharacteristicGroupState {
    selectedCharacteristicGroup: ICharacteristicGroup,
    count: number,
    characteristicGroups: Array<ICharacteristicGroupInfo>
}


export interface SearchCharacteristicGroupsAction {
    type: CharacteristicGroupActionTypes.SEARCH_CHARACTERISTIC_GROUPS,
    payload: ISearchCharacteristicGroups
}

export interface GetCharacteristicGroupsAction {
    type: CharacteristicGroupActionTypes.GET_CHARACTERISTIC_GROUPS,
    payload: Array<ICharacteristicGroupInfo>
}

export interface GetByIdCharacteristicGroupAction {
    type: CharacteristicGroupActionTypes.GET_BY_ID_CHARACTERISTIC_GROUP,
    payload: ICharacteristicGroup
}

export interface CreateCharacteristicGroupAction {
    type: CharacteristicGroupActionTypes.CREATE_CHARACTERISTIC_GROUP,
    payload: ICharacteristicGroup
}

export interface UpdateCharacteristicGroupAction {
    type: CharacteristicGroupActionTypes.UPDATE_CHARACTERISTIC_GROUP,
    payload: ICharacteristicGroup
}


export type CharacteristicGroupAction = SearchCharacteristicGroupsAction |
    GetCharacteristicGroupsAction |
    GetByIdCharacteristicGroupAction |
    CreateCharacteristicGroupAction |
    UpdateCharacteristicGroupAction;