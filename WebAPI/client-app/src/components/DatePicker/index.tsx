import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';

import { TextFieldStyle } from "./styled";

export interface IDatePicker {
    label: string,
    touched?: boolean | undefined,
    error?: string | undefined,
    value: Date | null,
    onChange: (e: any, value: any) => void,
};

const DatePickerComponent: React.FC<IDatePicker> = ({ value, onChange, label, touched, error }) => {
    return (
        <LocalizationProvider dateAdapter={AdapterDateFns}>
            <DatePicker
                value={value}
                onChange={onChange}
                renderInput={(props) => (
                    <TextFieldStyle
                        error={Boolean(touched && error)}
                        helperText={touched && error}
                        label={label}
                        variant="standard"
                        fullWidth
                        {...props}
                    />
                )}
            />
        </LocalizationProvider>
    );
}

export default DatePickerComponent;