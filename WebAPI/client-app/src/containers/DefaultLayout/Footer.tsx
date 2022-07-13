import Container from "@mui/material/Container";
import Grid from "@mui/material/Grid";

import PhoneIcon from '@mui/icons-material/Phone';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';

import FacebookIcon from '@mui/icons-material/Facebook';
import TelegramIcon from '@mui/icons-material/Telegram';
import InstagramIcon from '@mui/icons-material/Instagram';

import { BoxStyle, TitleStyle, DescriptionStyle, DescriptionLinkStyle } from "./styled";

const footers = [
    {
        title: 'Services and services',
        description: ['Service agreements', 'Gift certificates', 'Credit and payment in installments', 'Gift cards'],
    },

    {
        title: 'Partners',
        description: ['Shafa', 'Crafta.ua', 'Vsisvoi.ua', 'Izi.ua'],
    },
    {
        title: 'Assistance',
        description: ['Find an order', 'Guarantee', 'Service centers', 'Frequently asked questions'],
    },
];

const Footer = () => {
    return (
        <BoxStyle>
            <Container
                component="footer"
                sx={{
                    maxWidth: { xl: "xl", lg: "lg", md: "md" },
                    py: "110px"
                }}
            >
                <Grid container>
                    <Grid item xs={3}>
                        <TitleStyle>About us</TitleStyle>
                        <DescriptionStyle>
                            <FacebookIcon sx={{ fontSize: "30px", mr: "20px" }} />
                            <TelegramIcon sx={{ fontSize: "30px", mr: "20px" }} />
                            <InstagramIcon sx={{ fontSize: "30px", mr: "20px" }} />
                        </DescriptionStyle>
                        <DescriptionStyle><PhoneIcon sx={{ fontSize: "30px", mr: "10px" }} />+38054 824 67 31</DescriptionStyle>
                        <DescriptionStyle><LocationOnIcon sx={{ fontSize: "30px", mr: "10px" }} />Ukraine, str. Soborna 27</DescriptionStyle>
                        <DescriptionStyle><ShoppingCartIcon sx={{ fontSize: "30px", mr: "10px" }} />10:00 - 21:00 (No weekends)</DescriptionStyle>
                    </Grid>
                    {footers.map((footer) => (
                        <Grid item xs={3} key={`footer_${footer.title}`}>
                            <TitleStyle>{footer.title}</TitleStyle>
                            {footer.description.map((item) => (
                                <DescriptionLinkStyle href="#" key={`footer_item_${item}`}>{item}</DescriptionLinkStyle>
                            ))}
                        </Grid>
                    ))}
                </Grid>
            </Container>
        </BoxStyle>
    );
}

export default Footer;