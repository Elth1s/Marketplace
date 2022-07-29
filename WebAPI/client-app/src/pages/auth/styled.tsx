import Typography, { TypographyProps } from '@mui/material/Typography';
import Avatar, { AvatarProps } from '@mui/material/Avatar';
import { styled } from '@mui/material/styles';

export const AuthHeaderTypography = styled(Typography)<TypographyProps>(({ theme }) => ({
    height: "60px",
    fontSize: "45px",
}));



export const AuthAvatar = styled(Avatar)<AvatarProps>(({ theme }) => ({
    width: "45px",
    height: "45px",
    background: "#000",
    color: "#fff"
}));