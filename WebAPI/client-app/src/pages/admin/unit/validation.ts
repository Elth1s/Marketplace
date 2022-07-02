import * as Yup from 'yup';

export const UnitSchema = Yup.object().shape({
    measure: Yup.string().min(1).max(30).required().label('Measure'),
});