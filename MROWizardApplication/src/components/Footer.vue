<template>
  <div :class="$vuetify.breakpoint.xs ? 'footerBlockMobile' : 'footerBlock'">
    <v-row>
      <v-col cols="12" style="padding:0px">
        <span id="poweredby">Powered by </span>
        <a href="https://mrocorp.com/" target="_blank">
          <img alt="Qries" src="../assets/images/MRO-Web.png" height="30px">
        </a>
      </v-col>
      <v-col cols="12" style="padding:0px">
        <span id="footer"
          >For assistance, please call (610)994-7500 Option 1 M-F from 8:30 AM
          to 8:00 PM EST</span
        >
      </v-col>
    </v-row>
  </div>
</template>