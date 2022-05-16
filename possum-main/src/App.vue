<template>
<div>
<operator-mode v-if="false"></operator-mode>
<manager-mode></manager-mode>
</div>
</template>

<script>
import operatormode from './components/operator-mode.vue';
import managermode from './components/manager-mode.vue';
export default {
  name: 'App',
  components: {
    'operator-mode' : operatormode,
    'manager-mode': managermode},
  data() {
    return {
      
    }
  },
  methods : {
    
  }
}
</script>

<style>

</style>
