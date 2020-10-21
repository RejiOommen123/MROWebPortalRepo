<template>
  <div>
    <v-dialog
      v-model="dialog"
      max-width="250"
      light
    >
      <v-card>
        <v-card-title class="headline">Session Expired</v-card-title>

        <v-card-text>
          <p>You have left this browser idle for {{this.secondTime/1000}} seconds.</p>
          <p>{{ time/1000 }} second left</p>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="green darken-1"
            text
            @click="dialog = false"
          >
            ok
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
export default {
    name: 'SessionTimeout',
    data: function () {
        return {
            events :['click', 'mousemove', 'mousedown', 'scroll', 'keypress', 'load'],
            primaryTimer: null,
            secondaryTimer: null,
            firstTime:0,
            secondTime:0,
            time:0,
            warningZone: false,
            dialog:false
        }
    },
    mounted() {
        this.firstTime = this.$store.state.ConfigModule.nPrimaryTimeout * 1000;
        this.secondTime = this.$store.state.ConfigModule.nSecondaryTimeout * 1000;
        this.events.forEach(function (event) {
            window.addEventListener(event, this.resetTimer);
        }, this);
        this.setTimers();
    },
    destroyed() {
        this.events.forEach(function (event) {
            window.removeEventListener(event, this.resetTimer);
        }, this);
        this.resetTimer();
    },
     watch: {
      firstTimer(newfirstTimer) {
        this.firstTime = newfirstTimer * 1000;
      },
      secondTimer(newsecondTimer) {
        this.secondTime = newsecondTimer * 1000;
      }
    },
    computed: {
      firstTimer() {
        return this.$store.state.ConfigModule.nPrimaryTimeout;
      },
      secondTimer() {
        return this.$store.state.ConfigModule.nSecondaryTimeout;
      }
    },
    methods: {
        setTimers: function() {          
            this.primaryTimer = setTimeout(this.warningMessage, this.firstTime); // 14 minutes - 14 * 60 * 1000
            this.warningZone = false;
        },
        warningMessage: function() {    
          this.time = this.secondTime;      
          this.warningZone = true;
            let timerId = setInterval(() => {
                this.time -= 1000;
                if (!this.warningZone) clearInterval(timerId);
                if (this.time < 1) {
                  clearInterval(timerId);
                  this.dialog=false;
                  window.top.postMessage("hello", "*");
                  // Your logout function should be over here
                }
                // console.log(this.secondaryTimer);
              }, 1000);

            this.dialog = true;
        },
        logoutUser: function() {
            // Laravel            
            this.dialog = false;
            window.top.postMessage("hello", "*");
        },
        resetTimer: function() {
            clearTimeout(this.primaryTimer);
            // clearTimeout(this.secondaryTimer);
            this.time = this.secondTime;   
            this.dialog=false;
            this.setTimers();
        }
    }
}
</script>