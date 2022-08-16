import Box from '@mui/material/Box';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';

import { useTranslation } from 'react-i18next';

import Header from '../../containers/DefaultLayout/Header';

import LinkRouter from '../../components/LinkRouter';

const NotFound = () => {
    const { t } = useTranslation();

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
                <Box>
                    <Typography fontSize="150px" lineHeight="188px" fontWeight="600" sx={{ marginBottom: "20px" }}>404</Typography>
                    <Typography variant="h1" sx={{ marginBottom: "20px" }}>{`${t('pages.notFound.title')}`}</Typography>
                    <Typography variant="h4" fontWeight="medium" sx={{ marginBottom: "95px" }}>
                        {`${t('pages.notFound.descriptionTop')}`}
                        <br />
                        {`${t('pages.notFound.descriptionBottom')}`}
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
                            {`${t('pages.notFound.button')}`}
                        </Button>
                    </LinkRouter>
                </Box>
            </ Container>
        </>
    );
}

export default NotFound;