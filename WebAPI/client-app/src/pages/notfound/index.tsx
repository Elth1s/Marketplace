import {
    Container,
    Button,
    Typography
} from '@mui/material';

import Header from '../../containers/DefaultLayout/Header';

import LinkRouter from '../../components/LinkRouter';

const NotFound = () => {
    return (
        <>
            <Header />
            <Container component="main"
                sx={{
                    maxWidth: { xl: "xl", lg: "lg", md: "md" },
                    marginTop: "153px",
                    textAlign: "center",
                }}
            >
                <Typography fontSize="150px" lineHeight="188px" fontWeight="600" sx={{ marginBottom: "20px" }}>404</Typography>
                <Typography variant="h1" sx={{ marginBottom: "20px" }}>Page not found</Typography>
                <Typography variant="h4" fontWeight="medium" sx={{ marginBottom: "95px" }}>
                    Unfortunately, the page you asked for could not be found.
                    <br />
                    Please go back to the main page.
                </Typography>
                <LinkRouter underline="none" to="/">
                    <Button
                        variant="contained"
                        sx={{
                            fontSize: "20px",
                            lineHeight: "25px",
                            fontWeight: "500",
                            borderRadius: "10px",
                            px: "65px",
                            py: "15px",
                            textTransform: "none",
                        }}
                    >
                        Home page
                    </Button>
                </LinkRouter>
            </ Container>
        </>
    );
}

export default NotFound;