import ListItemButton from '@mui/material/ListItemButton';
import { styled } from '@mui/material/styles';

export const ListItemStyle = styled(ListItemButton)(({ theme }) => ({
    "&.Mui-selected": {
        backgroundColor: "transparent",
        "& .MuiTypography-root": {
            color: theme.palette.primary.main,
        },
    },
}));

