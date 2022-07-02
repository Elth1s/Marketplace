import { styled } from '@mui/material';
import {
    AppBar,
    Box
} from '@mui/material';

export const AppBarStyle = styled(AppBar)(({ theme }) => ({
    display: "flex",
    justifyContent: "center",
    position: "static",
    height: "80px",
    p: 0,
}));

export const LeftBox = styled(Box)(({ theme }) => ({
    display: "flex",
    justifyContent: "space-between",
    alignItems: 'center',
    width: "250px"
}));