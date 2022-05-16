<template>
  <div class="ui buttons">
      <a class="ui massive toggle button" v-bind:class="store" href="#">Operator</a>
      <a class="ui massive toggle button" v-bind:class="manager" href="#manager">Manager</a>
  </div>
</template>

<script>
export default {
  name : 'mode-switcher',
  methods : {
    is_operator() {
      return document.location.hash == "#manager" ? false : true;
    }
  },
  computed : {
    store() {
      return this.is_operator() ? "active" : "";
    },
    manager() {
      return this.is_operator() ? "" : "active";
    }
  }
}
</script>

<style>

</style>