import * as Yup from 'yup';

export const validationFields = Yup.object().shape({
    value: Yup.string().min(1).max(30).required().label('Value'),
    characteristicNameId: Yup.number().required().label('Characteristic name'),
});