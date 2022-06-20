import * as AuthActionCreators from "../../pages/auth/actions"
import * as ProfileActionCreators from "../../pages/user/actions"
import * as CategoryActionCreators from "../../pages/admin/category/actions"
import * as CharacteristicGroupActionCreators from "../../pages/admin/characteristicGroup/actions"
import * as CharacteristicActionCreators from "../../pages/admin/characteristic/actions"
import * as CountryActionCreators from "../../pages/admin/country/actions"
import * as CityActionCreators from "../../pages/admin/city/actions"

const actions = {
    ...AuthActionCreators,
    ...ProfileActionCreators,
    ...CategoryActionCreators,
    ...CharacteristicGroupActionCreators,
    ...CharacteristicActionCreators,
    ...CountryActionCreators,
    ...CityActionCreators,
}
export default actions;