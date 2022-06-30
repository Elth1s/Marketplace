import * as AuthActionCreators from "../../pages/auth/actions"
import * as ProfileActionCreators from "../../pages/user/actions"
import * as CategoryActionCreators from "../../pages/admin/category/actions"
import * as CharacteristicGroupActionCreators from "../../pages/admin/characteristicGroup/actions"
import * as CharacteristicNameActionCreators from "../../pages/admin/characteristicName/actions"
import * as CountryActionCreators from "../../pages/admin/country/actions"
import * as CityActionCreators from "../../pages/admin/city/actions"

const actions = {
    ...AuthActionCreators,
    ...ProfileActionCreators,
    ...CategoryActionCreators,
    ...CharacteristicGroupActionCreators,
    ...CharacteristicNameActionCreators,
    ...CountryActionCreators,
    ...CityActionCreators,
}
export default actions;