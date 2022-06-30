import * as Yup from 'yup';

export const validationFields = Yup.object().shape({
    name: Yup.string().min(2).max(30).required().label('Name'),
    characteristicGroupId: Yup.number().required().label('Characteristic group')
});