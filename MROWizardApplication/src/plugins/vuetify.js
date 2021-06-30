import Vue from 'vue';
import Vuetify from 'vuetify/lib';

Vue.use(Vuetify);

export default new Vuetify({
  theme: {
    themes: {
      options: { customProperties: true,  cspNonce: 'dQw4w9WgXcQ' },
      light: {
        primary: '#1976D2',
        secondary: '#424242',
        accent: '#82B1FF',
        error: '#FF5252',
        info: '#2196F3',
        success: '#4CAF50',
        warning: '#FFC107',
        customLightGrey: '#EBEEF3',
        customText: '#334150',
        customBlack: '#000000',
        customWhite: '#FFFFFF',
        customDarkBlue: '#0132D3',
        customYellow: '#F5A707'
      },
    },
  },
})